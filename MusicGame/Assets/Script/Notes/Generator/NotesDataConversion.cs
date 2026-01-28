using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Steamworks;

namespace Notes
{
    public class NotesDataConversion
    {
        private NotesData data;

        public NotesDataConversion(NotesData data)
        {
            this.data = data;
            DataConversion();
        }

        private void DataConversion()
        {
            for (int i = 0; i < data.notes.Length; i++)
            {
                int holdCnt = 0;
                Notes preKind = data.notes[i][0];

                for (int j = 1; j < data.notes[i].Count; j++)
                {
                    // 通常処理
                    {
                        if (data.notes[i][j].kind != preKind.kind || data.notes[i][j].dir != preKind.dir)
                        {
                            if (holdCnt > 0)
                            {
                                Notes pre = data.notes[i][j - 1];

                                int dir = (int)pre.dir;
                                if (dir < 0) dir = 0;

                                data.notes[i][j - 1] =
                                    new Notes(
                                        pre.time,
                                        dir,
                                        (int)pre.kind,
                                        holdCnt
                                    );

                                holdCnt = 0;
                            }

                            preKind = data.notes[i][j];
                            continue;
                        }
                    }

                    // 最後のノーツの処理
                    {
                        if (j == data.notes[i].Count - 1)
                        {
                            if (holdCnt > 0)
                            {
                                Notes pre = data.notes[i][j - 1];

                                int dir = (int)pre.dir;
                                if (dir < 0) dir = 0;

                                data.notes[i][j - 1] =
                                    new Notes(
                                        pre.time,
                                        dir,
                                        (int)pre.kind,
                                        holdCnt
                                    );

                                holdCnt = 0;

                                data.notes[i].RemoveAt(j);

                                continue;
                            }
                        }
                    }

                    // 加算処理
                    if (data.notes[i][j].kind != NotesKind.Flick)
                    {
                        data.notes[i].RemoveAt(j);
                        j--;
                        holdCnt++;
                    }
                }
            }
        }

        /// <summary>
        /// 読み取ったノーツデータのホールド部分を再編集
        /// </summary>
        /// <param name="data"></param>
        public static NotesData NotesDataReSize(NotesData data)
        {
            for (int i = 0; i < data.notes.Length; i++)
            {
                int holdCnt = 0;
                Notes preKind = data.notes[i][0];

                for (int j = 1; j < data.notes[i].Count; j++)
                {
                    // 通常処理
                    {
                        if (data.notes[i][j].kind != preKind.kind || data.notes[i][j].dir != preKind.dir)
                        {
                            if (holdCnt > 0)
                            {
                                Notes pre = data.notes[i][j - 1];

                                int dir = (int)pre.dir;
                                if (dir < 0) dir = 0;

                                data.notes[i][j - 1] =
                                    new Notes(
                                        pre.time,
                                        dir,
                                        (int)pre.kind,
                                        holdCnt
                                    );

                                holdCnt = 0;
                            }

                            preKind = data.notes[i][j];
                            continue;
                        }
                    }

                    // 最後のノーツの処理
                    {
                        if (j == data.notes[i].Count - 1)
                        {
                            if (holdCnt > 0)
                            {
                                Notes pre = data.notes[i][j - 1];

                                int dir = (int)pre.dir;
                                if (dir < 0) dir = 0;

                                data.notes[i][j - 1] =
                                    new Notes(
                                        pre.time,
                                        dir,
                                        (int)pre.kind,
                                        holdCnt
                                    );

                                holdCnt = 0;

                                data.notes[i].RemoveAt(j);

                                continue;
                            }
                        }
                    }

                    // 加算処理
                    if (data.notes[i][j].kind != NotesKind.Flick)
                    {
                        data.notes[i].RemoveAt(j);
                        j--;
                        holdCnt++;
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// ノーツデータの合体、計算した後に呼び出す
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static NotesData NotesDataSum(List<NotesData> data)
        {
            if (data == null) return null;
            if (data.Count == 1) { return data[0]; }

            NotesData notesData = data[0];

            for (int i = 1; i < data.Count; i++)
            {
                float Delay = data[i].BPM;
                float[] LastTime = new float[2]
                {
                    notesData.notes[0][(notesData.notes[0].Count - 1)].time,
                    notesData.notes[1][(notesData.notes[1].Count - 1)].time,
                };

                float lastTime = new();
                if (LastTime[0] > LastTime[1]) { lastTime = LastTime[0]; }
                else { lastTime = LastTime[1]; }

                for (int lane = 0; lane < 2; lane++)
                {
                    for (int index = 0; index < data[i].notes[lane].Count; index++)
                    {
                        float CreateTime = data[i].notes[lane][index].time;
                        CreateTime += lastTime + Delay;

                        Notes notes = new Notes(
                            time_: CreateTime,
                            dirN: (int)data[i].notes[lane][index].dir,
                            kind: (int)data[i].notes[lane][index].kind,
                            lenge: data[i].notes[lane][index].range
                            );

                        notesData.notes[lane].Add(notes);
                    }

                }
            }

            return notesData;
        }

        public static int TotalNotesScore(NotesData data,int MusicBPM)
        {
            int totalscore = 0;

            for (int i = 0; i < data.notes.Length; i++)
            {
                foreach (var notes in data.notes[i])
                {
                    // 幅の再定義
                    var average = 2.0f * MusicBPM / data.BPM;

                    var cnt = (int)(notes.range * average);

                    totalscore += (cnt + 1) * 2;
                }
            }
           
            return totalscore;
        }
    }
}

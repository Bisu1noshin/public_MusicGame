using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

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
                NotesKind preKind = data.notes[i][0].kind;

                for (int j = 0; j < data.notes[i].Count;j++)
                {
                    if (data.notes[i][j].kind != preKind)
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

                        preKind = data.notes[i][j].kind;
                        continue;
                    }

                    if (data.notes[i][j].kind != NotesKind.Flick)
                    {
                        data.notes[i].RemoveAt(j);
                        j--;
                        holdCnt++;
                    }                
                }
            }     
        }

        public NotesData GetData()
        {
            return data;
        }
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;

namespace OnLine
{
    public enum DirId
    {
        None = 0,
        UP, DOWN, LEFT, RIGHT
    };

    /// <summary>
    /// 部屋を特定するデータを保存するクラス
    /// </summary>
    [System.Serializable]
    public sealed class RoomId
    {
        /// <summary>
        /// 部屋を識別するようのID
        /// 4桁とする
        /// </summary>
        public DirId[] _RoomId { get; private set; }

        /// <summary>
        /// 部屋にかけるパスワードID
        /// </summary>
        public List<DirId> _PaswardID { get; private set; }

        /// <summary>
        /// パスワードがかかっているかどうかのフラグ
        /// </summary>
        public bool IsPasward { get; private set; }

        public RoomId()
        {
            _RoomId = new DirId[4];
            _PaswardID = new();
            IsPasward = false;
        }

        /// <summary>
        /// RoomIDを決める
        /// </summary>
        /// <param name="ids">配列は４!!</param>
        public void SetRoomID(DirId[] ids)
        {
            _RoomId = ids;
        }

        /// <summary>
        /// パスワードを決める
        /// </summary>
        /// <param name="ids"></param>
        public void SetPasward(DirId[] ids)
        {
            // 1度パスワードを確定させたら変更できなくする
            if (IsPasward) return;
            IsPasward = true;

            foreach (DirId id in ids)
            {
                _PaswardID.Add(id);
            }
        }
    }

}

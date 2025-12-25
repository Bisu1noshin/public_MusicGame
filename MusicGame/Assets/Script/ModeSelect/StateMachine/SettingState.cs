using Notes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModeSelect.StateMachine
{
    public class SettingState : IState
    {
        NotesManagerPlayerConfig mDataBase;
        PropertyController mProperty;
        PopupController mPopup_NotesSpeed;
        int mPrevLayer;
        float mCurrSpeed;
        public SettingState(ModeSelectSceneManager owner, IStateMachine<Trigger> st) : base(owner, st)
        {
            ReserveNullActionList(1);
            mDataBase = mOwner.GetNotesManager().PlayerConfig;
            mPrevLayer = 0;
            InitDic();
        }
        protected override void OnEnter()
        {
            SetupLayer0();
        }
        protected override void OnUpdate(float deltaTime)
        {
            //Layerが変わったとき
            if (mPrevLayer != layer)
            {
                if (mOwner.ExistCursol) { mOwner.DeleteCursol(); }
                if (mPrevLayer < layer)
                {
                    SelectNum[layer] = 0;
                    ReplaceEnterAction(Actions[SelectNum[layer]]);
                }
                if (mPrevLayer == 1 && layer == 0)
                {
                    SetupLayer0();
                }
            }
            //カーソル操作
            if (layer == 0)
            {
                SetPropertyText(SelectNum[0]);
                if (mOwner.ExistCursol) mOwner.CursolRect.anchoredPosition = new(-350.0f, -(SelectNum[0] - (6 - 1) / 2.0f) * (540 / 6 * 2));
            }

            if (layer == 1 && mPopup_NotesSpeed != null)
            {
                mPopup_NotesSpeed.SetValue(mCurrSpeed);
            }
            mPrevLayer = layer;
        }
        protected override void OnExit()
        {
            mOwner.DeleteCursol();
        }

        /// <summary>
        /// 　Dictionary初期化　派生先の挙動を登録する
        /// 　すっごい見にくいのはご容赦
        /// </summary>
        void InitDic()
        {
            //ActionDic.Add(0, () =>
            //{
            //    ReplaceNullActionList(0);
            //    layer++;
            //    deleteAction += PopupController.CreateInstance(this,
            //    "オートプレイを" + (mDataBase.AutoPlay ? "OFF" : "ON") + "にします。\nよろしいですか？", () => { mDataBase.AutoPlay = !mDataBase.AutoPlay; layer--; });
            //    Player.backAction = () => { deleteAction?.Invoke(); layer--; };
            //});

            //ActionDic.Add(1, () => { ReplaceNullActionList(0); layer++; deleteAction += PopupController.CreateInstance(this, "レーン反転を" + (mDataBase.LaneCahge ? "OFF" : "ON") + "にします。\nよろしいですか？", () => { mDataBase.LaneCahge = !mDataBase.LaneCahge; layer--; }); Player.backAction = () => { deleteAction?.Invoke(); layer--; }; });
            //ActionDic.Add(2, () => { ReplaceNullActionList(0); layer++; deleteAction += PopupController.CreateInstance(this, "操作の上下反転を" + (mDataBase.UpDownCahge ? "OFF" : "ON") + "にします。\nよろしいですか？", () => { mDataBase.UpDownCahge = !mDataBase.UpDownCahge; layer--; }); Player.backAction = () => { deleteAction?.Invoke(); layer--; }; });
            //ActionDic.Add(3, () => { ReplaceNullActionList(0); layer++; deleteAction += PopupController.CreateInstance(this, "操作の左右反転を" + (mDataBase.LeftRightCahge ? "OFF" : "ON") + "にします。\nよろしいですか？", () => { mDataBase.LeftRightCahge = !mDataBase.LeftRightCahge; layer--; }); Player.backAction = () => { deleteAction?.Invoke(); layer--; }; });
            //ActionDic.Add(4, () => { ReplaceNullActionList(0); layer++; deleteAction += PopupController.CreateInstance(this, "操作デバイスを" + (mDataBase.InputDevice == InputDevice.Controller ? "キーボード" : "コントローラー") + "にします。\nよろしいですか？", () => { mDataBase.InputDevice = mDataBase.InputDevice == InputDevice.Controller ? InputDevice.KyeBord : InputDevice.Controller; layer--; }); Player.backAction = () => { deleteAction?.Invoke(); layer--; }; });
            //ActionDic.Add(5, () => { ReplaceNullActionList(0); layer++; (PopupController, Action) tuple = PopupController.CreateInstanceForNotesSpeed(mDataBase.InputDevice == InputDevice.Controller); mPopup_NotesSpeed = tuple.Item1; deleteAction += () => { tuple.Item2.Invoke(); mPopup_NotesSpeed = null; }; mCurrSpeed = mDataBase.NotesSpeed; Player.backAction = () => { deleteAction?.Invoke(); layer--; }; Actions.Add(() => { mDataBase.NotesSpeed = mCurrSpeed; layer--; }); Player.vecAction = (Vector2) => ChangeCurrSpeed(Vector2); });
            ActionDic.Add(0, CreateDicAction(
                () =>
                //入った時の挙動
                {
                    layer++;
                    deleteAction += PopupController.CreateInstance(this,
                    "オートプレイを" + (mDataBase.AutoPlay ? "OFF" : "ON") + "にします。\nよろしいですか？");
                },
                //決定した時の挙動
                () => { mDataBase.AutoPlay = !mDataBase.AutoPlay; layer--; },
                //戻る時の挙動
                () => { layer--; }
            ));
            ActionDic.Add(1, CreateDicAction(
                () =>
                {
                    layer++;
                    deleteAction += PopupController.CreateInstance(this,
                        "レーン反転を" + (mDataBase.LaneCahge ? "OFF" : "ON") + "にします。\nよろしいですか？");
                },
                () => { mDataBase.LaneCahge = !mDataBase.LaneCahge; layer--; },
                () => { layer--; }
            ));
            ActionDic.Add(2, CreateDicAction(
                () =>
                {
                    layer++;
                    deleteAction += PopupController.CreateInstance(this,
                        "操作の上下反転を" + (mDataBase.UpDownCahge ? "OFF" : "ON") + "にします。\nよろしいですか？");
                },
                () => { mDataBase.UpDownCahge = !mDataBase.UpDownCahge; layer--; },
                () => { layer--; }
            ));
            ActionDic.Add(3, CreateDicAction(
                () =>
                {
                    layer++;
                    deleteAction += PopupController.CreateInstance(this,
                        "操作の左右反転を" + (mDataBase.LeftRightCahge ? "OFF" : "ON") + "にします。\nよろしいですか？");
                },
                () => { mDataBase.LeftRightCahge = !mDataBase.LeftRightCahge; layer--; },
                () => { layer--; }
            ));
            ActionDic.Add(4, CreateDicAction(
                () =>
                {
                    layer++;
                    deleteAction += PopupController.CreateInstance(this,
                        "操作デバイスを" + (mDataBase.InputDevice == InputDevice.Controller ? "キーボード" : "コントローラー") + "にします。\nよろしいですか？");
                },
                () => { mDataBase.InputDevice = mDataBase.InputDevice == InputDevice.Controller ? InputDevice.KyeBord : InputDevice.Controller; layer--; },
                () => { layer--; }
            ));
            ActionDic.Add(5, CreateDicAction(
                () =>
                {
                    layer++;
                    (PopupController, Action) tuple =
                    PopupController.CreateInstanceForNotesSpeed(mDataBase.InputDevice == InputDevice.Controller);
                    mPopup_NotesSpeed = tuple.Item1;
                    deleteAction += () => { tuple.Item2.Invoke(); mPopup_NotesSpeed = null; };
                    mCurrSpeed = mDataBase.NotesSpeed;
                },
                () => { mDataBase.NotesSpeed = mCurrSpeed; layer--; Player.vecAction = (Vector2) => Scroll(Vector2); },
                () => { layer--; Player.vecAction = (Vector2) => Scroll(Vector2); },
                () => Player.vecAction = (Vector2) => ChangeCurrSpeed(Vector2)
            ));
        }

        //初期画面のセット
        void SetupLayer0()
        {
            ReplaceNullActionList(6);
            deleteAction = null;
            deleteAction += Button.ButtonManager.CreateInstance(this, 0, 6, "オートプレイ", ActionDic.GetValueOrDefault(0));
            deleteAction += Button.ButtonManager.CreateInstance(this, 1, 6, "レーン反転", ActionDic.GetValueOrDefault(1));
            deleteAction += Button.ButtonManager.CreateInstance(this, 2, 6, "上下反転", ActionDic.GetValueOrDefault(2));
            deleteAction += Button.ButtonManager.CreateInstance(this, 3, 6, "左右反転", ActionDic.GetValueOrDefault(3));
            deleteAction += Button.ButtonManager.CreateInstance(this, 4, 6, "デバイス変更", ActionDic.GetValueOrDefault(4));
            deleteAction += Button.ButtonManager.CreateInstance(this, 5, 6, "ノーツ速度", ActionDic.GetValueOrDefault(5));
            (PropertyController, Action) tuple = PropertyController.CreateInstance();
            mProperty = tuple.Item1;
            deleteAction += tuple.Item2;

            Player.backAction = () => { deleteAction?.Invoke(); mOwner.mStateMachine.ExecuteTriggerAction(Trigger.Home); };
            Player.vecAction = (Vector2) => Scroll(Vector2);
            mOwner.CreateCursol();
            ReplaceEnterAction(Actions[SelectNum[0]]);
        }

        /// <summary>
        /// 　ノーツ速度を変える関数　vecActionに突っ込む
        /// 　現在は0.1単位で変更できる
        /// </summary>
        void ChangeCurrSpeed(Vector2 vec)
        {
            mCurrSpeed += vec.y * 0.1f;
            //mCurrSpeed = ((int)(mCurrSpeed * 10.0f)) / 10;
            if (mCurrSpeed > 3.0f) { mCurrSpeed = 3.0f; }
            if (mCurrSpeed < 0.3f) { mCurrSpeed = 0.3f; }
        }

        //ホーム画面の説明を管理
        void SetPropertyText(int v)
        {
            if (mProperty == null) return;
            string str;
            switch (v)
            {
                case 0:
                    str = "オートプレイの設定ができます。\n現在：" + (mDataBase.AutoPlay ? "ON" : "OFF");
                    break;
                case 1:
                    str = "レーン反転の設定ができます。\n現在：" + (mDataBase.LaneCahge ? "ON" : "OFF");
                    break;
                case 2:
                    str = "操作の上下反転の設定ができます。\n現在：" + (mDataBase.UpDownCahge ? "ON" : "OFF");
                    break;
                case 3:
                    str = "操作の左右反転の設定ができます。\n現在：" + (mDataBase.LeftRightCahge ? "ON" : "OFF");
                    break;
                case 4:
                    str = "デバイスの変更ができます。\n現在：" + (mDataBase.InputDevice == InputDevice.Controller ? "コントローラー" : "キーボード");
                    break;
                case 5:
                    str = "ノーツ速度の設定ができます。\n現在：" + mDataBase.NotesSpeed.ToString();
                    break;
                default:
                    str = string.Empty;
                    break;
            }
            mProperty.SetText(str);
        }

        Action CreateDicAction(Action init, Action enter, Action back, Action vec = null)
        {
            Action _enter = () => {
                Actions.Clear();
                Actions.Add(enter);
                ReplaceEnterAction(Actions[0]);
            };
            Action _back = () => {
                Player.backAction = () => {
                    deleteAction?.Invoke();
                    back?.Invoke();
                };
            };
            Action ret = () => {
                _enter.Invoke();
                init?.Invoke();
                _back.Invoke();
                vec?.Invoke();
            };
            return ret;
        }
    }
}
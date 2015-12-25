using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRSkipAsync;

namespace GetkenAsync
{
    public class CS_GetkenAsync
    {
        #region 共有領域
        CS_RskipAsync rskip;             // 右側余白情報を削除
        CS_LskipAsync lskip;             // 左側余白情報を削除

        private String _wbuf;       // ソース情報
        private Boolean _empty;     // ソース情報有無
        private int _wcnt;          // トークン登録数
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
                    // 不要情報削除
                    if (rskip == null || lskip == null)
                    {   // 未定義？
                        rskip = new CS_RskipAsync();
                        lskip = new CS_LskipAsync();
                    }
                    rskip.Wbuf = _wbuf;
                    rskip.Exec();
                    lskip.Wbuf = rskip.Wbuf;
                    lskip.Exec();
                    _wbuf = lskip.Wbuf;

                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        public String[] Array;      // トークン抽出情報
        public int Wcnt
        {
            get
            {
                return _wcnt;
            }
            set
            {
                _wcnt = value;
            }
        }
        private char[] _trim = { ' ', '\t', '\r', '\n' };
        #endregion

        #region コンストラクタ
        public CS_Getken()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            rskip = null;
            lskip = null;
        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            rskip = null;
            lskip = null;
        }
        public void Exec()
        {   // Token抽出（固定区切り）
            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(_trim);         // トークン抽出
                _wcnt = Array.Count<String>();      // 要素数取り出し
            }
        }
        public void Exec(char[] __trim)
        {   // Token抽出（指定区切り）
            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(__trim);
                _wcnt = Array.Count<String>();
            }
        }
        #endregion
    }
}

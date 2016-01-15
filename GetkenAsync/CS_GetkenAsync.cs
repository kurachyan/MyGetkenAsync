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
        // '16.01.13 両側余白情報削除の追加　及び、右側・左側余白処理のコメント化
        CS_LRskipAsync lrskip;           // 両側余白情報を削除

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
                    if (lrskip == null)
                    {   // 未定義？
                        lrskip = new CS_LRskipAsync();
                    }
                    lrskip.ExecAsync(_wbuf);
                    _wbuf = lrskip.Wbuf;

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
        public CS_GetkenAsync()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            lrskip = null;
        }
        #endregion

        #region モジュール
        public async Task ClearAsync()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            lrskip = null;
        }

        public async Task ExecAsync()
        {   // Token抽出（固定区切り）
            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(_trim);         // トークン抽出
                _wcnt = Array.Count<String>();      // 要素数取り出し
            }
        }
        public async Task ExecAsync(String msg)
        {   // Token抽出（固定区切り）
            await SetbufAsync(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(_trim);         // トークン抽出
                _wcnt = Array.Count<String>();      // 要素数取り出し
            }
        }
        public async Task ExecAsync(char[] __trim)
        {   // Token抽出（指定区切り）
            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(__trim);
                _wcnt = Array.Count<String>();
            }
        }
        public async Task ExecAsync(String msg, char[] __trim)
        {   // Token抽出（指定区切り）
            await SetbufAsync(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有り
                Array = _wbuf.Split(__trim);
                _wcnt = Array.Count<String>();
            }
        }

        private async Task SetbufAsync(String _strbuf)
        {   // [_wbuf]情報設定
            _wbuf = _strbuf;

            if (_wbuf == null)
            {   // 設定情報は無し？
                _empty = true;
            }
            else
            {   // 整形処理を行う
                // 不要情報削除
                if (lrskip == null)
                {   // 未定義？
                    lrskip = new CS_LRskipAsync();
                }
                await lrskip.ExecAsync(_wbuf);
                _wbuf = lrskip.Wbuf;

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
        #endregion
    }
    //  FIXME : Splitを使用すると、区切り情報も要素として見る。
    //          区切り情報単体は排除する処理も追加する。
}
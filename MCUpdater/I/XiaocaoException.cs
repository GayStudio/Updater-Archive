using System;

namespace MCUpdater.I
{
    class XiaocaoException : Exception
    {
        public new string Message = "由于您过于智障而引发此异常（您的智商水平 <= 小曹2015的智商水平）";
        public new string HelpLink = "https://www.google.com.hk/search?q=智商充值";

        public override string ToString()
        {
            throw new XiaocaoException();
        }
    }
}

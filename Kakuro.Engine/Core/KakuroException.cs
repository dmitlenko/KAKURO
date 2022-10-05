using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Core
{
    /**
     * <summary>Class for kakuro exceptions</summary>
     */
    public class KakuroException : Exception
    {
        /**
         * <summary>Error message</summary>
         */
        public string ErrorMessage;

        /**
         * <summary>Default constructor for KakuroException</summary>
         */
        public KakuroException() : base() { }

        /**
         * <summary>Constructor for KakuroException</summary>
         * <param name="message">Error message</param>
         */
        public KakuroException(string message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}

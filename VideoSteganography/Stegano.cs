using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoSteganography
{
    internal class Stegano
    {
        private string coverText;
        private string coveredMessage;

        public Stegano() 
        { 
        }

        public void setCoverText(string coverText)
        {
            this.coverText = coverText;
        }

        public string getCoverText()
        {
            return this.coverText;
        }

        
        public void setCoveredMessage(string coveredMessage)
        {
            this.coveredMessage = coveredMessage;
        }

        public string getCoveredMessage()
        {
            return this.coveredMessage;
        }
    }
}

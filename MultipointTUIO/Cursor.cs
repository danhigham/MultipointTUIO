using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MTMultiMouse
{
    class Cursor
    {
        public Point Position;
        public long FingerID;
        public int SessionID;

        public Cursor(Point Position, long FingerID, int SessionID) {
            this.Position = Position;
            this.FingerID = FingerID;
            this.SessionID = SessionID;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShroomDB
{
    public enum FlagSemantics
    {
        IsNegativeOffset = 0,
        IsSegmentEnd = 1,
        IsContinueOnPage = 2,
        IsContinueCrossPage = 3
    }
    public class Segment
    {

    }
    public class ShroomPointer
    {
        public int Page {  get; set; }
        public byte[] Offset { get; set; }
    }
    public class SegmentHeader
    {
        byte _flags;
        
    }
    public class Slice
    {

    }
}

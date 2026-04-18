using System;

namespace DistLearn;

public class GradeEventArgs : EventArgs
{
    public Grade Grade {get; set;}

    public GradeEventArgs()
    {
        Grade = null;
    }
}
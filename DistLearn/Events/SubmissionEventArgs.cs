using System;

namespace DistLearn;

public class SubmissionEventArgs : EventArgs
{
    public Submission Submission {get; set;}

    public SubmissionEventArgs()
    {
        Submission = null;
    }
}
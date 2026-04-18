using System;

namespace DistLearn;

public class DeadlineEventArgs : EventArgs
{
    public DateTime OldDeadline {get; set;}

    public DateTime NewDeadline {get; set;}

    public DeadlineEventArgs()
    {
        OldDeadline = DateTime.Now;
        NewDeadline = DateTime.Now;
    }
}
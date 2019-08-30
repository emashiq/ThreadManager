namespace ThreadManager
{
    public enum ThreadOperationMode
    {
        //Will work one after another in finishing state
        StartAndJoin = 0,
        //All will run simultaneously
        AllStartThenJoin = 1,
        //Will dispatch before main thread dispatching
        JoinOnMainThreadEnd = 2,
        //Run threads based on custom conditions
        CustomThreadSequence = 3
    }
}

namespace DesktopShortcutLauncher
{
    public class Empty { }
    public class Result<T>
    {
        public virtual T Get()
        {
            throw new Exception("Not Implemented.");
        }

        public class Success : Result<T>
        {
            private T value;
#pragma warning disable CS8601 // Possible null reference assignment.
            public Success(T value = default(T))
#pragma warning restore CS8601 // Possible null reference assignment.
            {
                this.value = value;
            }

            public override T Get()
            {
                return value;
            }
        }
        public class Failure : Result<T>
        {
            private Exception error;
            public Failure(Exception error)
            {
                this.error = error;
            }

            public override T Get()
            {
                throw error;
            }
        }

    }
}

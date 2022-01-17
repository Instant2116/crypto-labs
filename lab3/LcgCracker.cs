namespace lab3
{
    class LcgCracker
    {
        private long a;
        private long c;
        public static readonly long m = 4294967296;
        private long _last;

        public LcgCracker(long a, long c, long last)
        {
            this.a = a;
            this.c = c;
            _last = last;
        }

        public long Next()
        {
            _last = (a * _last + c) % m;
            return _last;
        }
    }
}
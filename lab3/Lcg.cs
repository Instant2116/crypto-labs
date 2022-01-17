namespace lab3
{
    class Lcg
    {
        private long a;
        private long c;
        public const long m = 4294967296;
        private long _last;

        public Lcg(long a, long c, long seed)
        {
            this.a = a;
            this.c = c;
            _last = seed;
        }

        public long Next()
        {
            _last = (a * _last + c) % m;
            return _last;
        }
    }
}
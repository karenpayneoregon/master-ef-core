using System;

namespace BaseUnitTestProject.LanguageExtensions
{
    public static partial class LanguageExtensions
    {
        public static int GetLength(this int sender) => sender switch
        {
            < 0 => throw new ArgumentOutOfRangeException(),
            0 => 1,
            _ => (int)Math.Floor(Math.Log10(sender)) + 1
        };

        public static int GetLengthConventional(this int sender)
        {
            switch (sender)
            {
                case < 0:
                    throw new ArgumentOutOfRangeException();
                case 0:
                    return 1;
                default:
                    return (int)Math.Floor(Math.Log10(sender)) + 1;
            }
        }


    }
}

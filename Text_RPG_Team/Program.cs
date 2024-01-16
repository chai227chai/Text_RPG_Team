using System.Runtime.InteropServices;

namespace Text_RPG_Team
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            GameManager gameManager = new GameManager();
            while (true)
            {
                gameManager.MainTown();
            }
        }
    }
}
using System;
using System.Linq;

namespace CashDispenser
{
    class Program
    {
        /*
         Метод для получения комбинаций купюр данной суммы.

         @param sum - переменная для передачи суммы
         @param prevNominal - переменная для хранения предыдущего номинала, участвующего в размене.
         @param combination - строка для хранения и вывода комбинации
         @param denomArr - массив номиналов
         @return число комбинаций
         */
        static Int32 Сombinations(Int32 sum, Int32 prevNominal, String combination, Int32[] denomArr)
        {
            Int32 count = 0;
            if (sum == 0)
            {
                count++;
                Console.WriteLine(combination);
            }
            for (int j = 0; j < denomArr.Length; j++)
            {
                if ((prevNominal >= denomArr[j]) && (sum >= denomArr[j]))
                {
                    count += Сombinations(sum - denomArr[j], denomArr[j], combination + " " + denomArr[j] + " ", denomArr);
                }
            }
            return count;
        }
        /*
         Метод для ввода суммы.

         @param str - строка для ввода суммы
         @return сумма
         */
        static Int32 SumIn(string str)
        {
            int sum;
            try
            {
                sum = Int32.Parse(str);
                if (sum <= 0)
                {
                    throw new ArithmeticException(" - ОШИБКА - поймали Exception при вводе суммы, сумма <=0 ?!");
                }
            }
            catch (FormatException e)
            {
                throw new ArithmeticException(" - ОШИБКА - поймали Exception при вводе суммы");
            }
            return sum;
        }
        /*
        Метод для ввода номиналов.

        @param str - строка с номиналами, введеными с консоли
        @return массив номиналов
        */
        static Int32[] NomIn(string str)
        {
            String[] initialStrMas = str.Split(" 0");
            String[] strMas = initialStrMas[0].Split(" ");
            Int32[] denomArr = new Int32[strMas.Length];
            for (int i = 0; i < strMas.Length; i++)
            {
                try
                {
                    Boolean shouldBeTrue = Int32.TryParse(strMas[i], out denomArr[i]);
                    if (denomArr[i] <= 0)
                    {
                        throw new ArithmeticException(" - ОШИБКА - поймали Exception при вводе номиналов, какой-то <=0 ?!");
                    }
                }
                catch (Exception e)
                {
                    throw new ArithmeticException(" - ОШИБКА - поймали Exception при вводе номиналов");
                }

            }
            //в метод для подсчета комбинаций, нужно отправлять массив в порядке убывания номиналов
            int[] res = denomArr.OrderByDescending(x => x).ToArray();
            return (res);
        }
        /**
         * Main.
         * Программа принимает от пользователя две строки из консоли
         * str1 - сумма
         * str2 - номиналы через пробел, признак конца строки " 0"
         * выводит комбинации и их число
         */
        static void Main(string[] args)
        {
            String sum = Console.ReadLine();
            String denominations = Console.ReadLine();
            Int32[] denominationsMas = NomIn(denominations);
            String specifier = "G";
            String str = Сombinations(SumIn(sum), denominationsMas[0], " ", denominationsMas).ToString(specifier);
            Console.WriteLine($"Всего комбинаций: {str}");
        }
    }
}

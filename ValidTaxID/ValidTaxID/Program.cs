using System;
using System.Text.RegularExpressions;

namespace ValidTaxID
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("請輸入統編:");
            var taxId = Console.ReadLine();
            var result = IsValidEIN(taxId);
            Console.WriteLine($"驗證結果:{result}");
            Console.ReadKey();
        }

        public static bool IsValidEIN(string value)
        {
            bool flag = false;
            // 檢查統編是否為8位數字字串
            if (Regex.IsMatch(value, @"^\d{8}$"))
            {
                int[] multiple = { 1, 2, 1, 2, 1, 2, 4, 1 };
                int sum = 0, tmp = 0, tmp2 = 0;
                for (int i = 0; i < multiple.Length; i++)
                {
                    //依不同位置進行不同數字的運算
                    int num = Convert.ToInt32(value.Substring(i, 1));
                    tmp = num * multiple[i];
                    // 乘除來的數字若為兩位數，把十位與個位數字相加
                    tmp2 = ((tmp / 10) + (tmp % 10));
                    // 將處理過後的數字作加總
                    sum += tmp2;
                }
                // 1.若數字可被10整除即驗證通過 2.若數字無法被10整除，但第7位數字 == 7 並且 加總的數字+1後可被10整除 也是正確的統編
                if (sum % 10 == 0 || (value.Substring(6, 1) == "7" && sum % 10 == 9))
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}
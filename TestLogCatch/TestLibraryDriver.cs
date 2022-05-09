using Common.Logging;
using System;

namespace TestLogCatch
{
    public class TestLibraryDriver
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ThrowError(int id)
        {
            try
            {
                LogWriter.LogInformation("Raxit " + id.ToString());

                if (id <= 3)
                {
                    throw new Exception($"id cannot be less than or equal to 3. value passed is {id}");
                }

                return id.ToString();
            }
            catch (Exception ex)
            {
                LogWriter.LogError(ex);
            }
            return string.Empty;
        }
    }
}

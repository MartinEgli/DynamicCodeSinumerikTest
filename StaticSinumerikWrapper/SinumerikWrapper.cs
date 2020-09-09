using System;
using SiemensMCSinumerikSolutionLineWrapper;
using SinumerikWrapperInterfaces;

namespace StaticSinumerikWrapper
{
    public class SinumerikWrapper : ISinumerikWrapper
    {
        private const int MAGIC_NUMBER_CONNECTED = 30;

        public bool CheckConnection()
        {
            try
            {
                int result;
                using (var dataSvc = new DataSvcWrapper(string.Empty))
                {
                    var status = new DataSvcStatusWrapper();

                    var dataSvcItem = new DataSvcItem("connect_state", string.Empty, null);

                    dataSvc.Read(ref dataSvcItem, 500, 0, true, ref status, false);
                    result = Convert.ToInt32(dataSvcItem.Value);
                }

                if (result == MAGIC_NUMBER_CONNECTED) return true;
            }
            catch (DataSvcException)
            {
            }
            catch (InvalidCastException)
            {
            }
            catch (FormatException)
            {
            }

            return false;
        }
    }
}
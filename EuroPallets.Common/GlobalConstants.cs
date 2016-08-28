

namespace EuroPallets.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalConstants
    {

        public const string AdministratorRoleName = "Administrator";
        public const string CustomerRoleName = "Customer";

        public const string SuccessfullRegistration = "Successfull Registration";

#if DEBUG
        public const string EmailtemplateFolder = "../../Users/LapTop/Documents/GitHub/EuroPalletsQAS/EuroPallets/Content/EmailEditor/";
#else
        public const string EmailtemplateFolder = "C:\\...\\...\\...\\Content\\EmailEditor\\";
#endif
    }
}

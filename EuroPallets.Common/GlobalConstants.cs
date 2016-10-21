

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
        public const string SuccessfullAddProductToCard = "Successfull Add Product To Cart";

        public const string RegistrationIsNeeded = "You need to create account to procceed this action !";

        public const string FaliedToAddNewEuroPaletFurniture = "Error when try to add new product";
        public const string SuccessNewEuroPaletFurniture = "Product was added successfully";

#if DEBUG
        public const string EmailtemplateFolder = "../../Users/Daniel/Documents/GitHub/EuroPallets/EuroPallets/Content/EmailEditor/";
        //public const string EmailtemplateFolder = "../../Users/LapTop/Documents/GitHub/EuroPalletsQAS/EuroPallets/Content/EmailEditor/";
#else
        public const string EmailtemplateFolder = "C:\\...\\...\\...\\Content\\EmailEditor\\";
#endif
    }
}

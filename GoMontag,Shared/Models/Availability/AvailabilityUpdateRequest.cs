using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Availability
{
     public record struct AvailabilityUpdateRequest(
         int FormatId,
         int StoreId,
         string URL,
         string UnitSold,
         string CurrencyCode,
         decimal Price
         );
}

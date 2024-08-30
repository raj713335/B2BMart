using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.Models.Enums
{
    public enum OrderItemStatus
    {
        [EnumMember(Value = "PLACED")]
        PLACED,
        [EnumMember(Value = "PACKED")]
        PACKED,
        [EnumMember(Value = "READY_TO_PICKUP")]
        READY_TO_PICKUP,
        [EnumMember(Value = "IN_TRANSIT")]
        IN_TRANSIT,
        [EnumMember(Value = "REACHED_DESTINATION_HUB")]
        REACHED_DESTINATION_HUB,
        [EnumMember(Value = "OUT_FOR_DELIVERY")]
        OUT_FOR_DELIVERY,
        [EnumMember(Value = "DELAYED")]
        DELAYED,
        [EnumMember(Value = "DELIVERED")]
        DELIVERED
    }
}

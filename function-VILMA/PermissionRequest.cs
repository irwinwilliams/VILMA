using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace function_shelly3
{
    public class PermissionRequest
    {
        private dynamic payload = new ExpandoObject();
        public PermissionRequest()
        {
            this.payload = new ExpandoObject();
            this.payload.google = new ExpandoObject();
            var google = this.payload.google;
            google.expectUserResponse = true;
            google.systemIntent = new ExpandoObject();
            var sysIntent = google.systemIntent;
            sysIntent.intent = "actions.intent.PERMISSION";
            sysIntent.data = new data();
            var data = sysIntent.data;
            data.type = "type.googleapis.com/google.actions.v2.PermissionValueSpec";
            data.optContext = "To deliver your order";
            data.permissions = new string[] { "UPDATE" };
            //data.permissions = new string[] { "NAME", "DEVICE_PRECISE_LOCATION", "UPDATE" };

        }

        public ExpandoObject GetPayload()
        {
            return payload;
        }
    }
}


//{
//  "payload": {
//    "google": {
//      "expectUserResponse": true,
//      "systemIntent": {
//        "intent": "actions.intent.PERMISSION",
//        "data": {
//          "@type": "type.googleapis.com/google.actions.v2.PermissionValueSpec",
//          "optContext": "To deliver your order",
//          "permissions": [
//            "NAME",
//            "DEVICE_PRECISE_LOCATION"
//          ]
//        }
//      }
//    }
//  }
//}
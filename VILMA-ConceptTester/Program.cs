using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace VILMA_ConceptTester
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string c = "";
            var token = "eyJ0eXAiOiJKV1QiLCJub25jZSI6ImREVDFoWW1sUHNWNmFWM3hXWmNKVlJYNHo4Zk95aEdIVGV1ZzBMd29wYnMiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83YjVjMjQ3Ny1mNzE3LTQ4Y2QtYWEyOC0yNGNlZTRlNDY0ZGMvIiwiaWF0IjoxNTY5NTUxNTg2LCJuYmYiOjE1Njk1NTE1ODYsImV4cCI6MTU2OTU1NTQ4NiwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhNQUFBQTVyM0N4aEFhSjlLd2xwamYxNGtadGZUdDNyYy9RWXNNeTFuWVZzalBLN2IwWFlPMU1aN1ByNjcyQThHbW1XRXNBcXR1eTVEQU9kM1pjRVFybmJVU0kzREJWTmJUVlBwd1QybldNaUQ3Q2pvPSIsImFtciI6WyJwd2QiLCJyc2EiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiVklMTUFhZ2VudGJvdCIsImFwcGlkIjoiZWMzYmU4MWYtMzA0Yi00YTNkLWJhMjYtMzQ0N2Y5YmNiMmUyIiwiYXBwaWRhY3IiOiIxIiwiZGV2aWNlaWQiOiJiNGZmNzY0Yi1kYjdjLTRhYmUtYjE2OC05Y2ZkNzQ2NGFmZmYiLCJmYW1pbHlfbmFtZSI6IldpbGxpYW1zIiwiZ2l2ZW5fbmFtZSI6Iklyd2luIiwiaXBhZGRyIjoiMTcwLjg0LjguMjExIiwibmFtZSI6Iklyd2luIFdpbGxpYW1zIiwib2lkIjoiMGY3YWIwODYtYTU4MS00MGI5LTg3ZTgtMGU0YmRkYmRkNWQ2Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTEzMTg3MjM2MDgtMzMxMTAyMjU4Ny0xNDgxOTgxMDU4LTEyMzMiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzdGRkU4MEUwQ0NCNyIsInNjcCI6IlBlb3BsZS5SZWFkIFVzZXIuUmVhZEJhc2ljLkFsbCBwcm9maWxlIG9wZW5pZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJkdmNfbW5nZCIsImttc2kiXSwic3ViIjoiM1pOYmdWOHR3OC1SUlQxYU9sWk56c2FOOGpoU0R1LVJ6d0lac1dBZ1dyayIsInRpZCI6IjdiNWMyNDc3LWY3MTctNDhjZC1hYTI4LTI0Y2VlNGU0NjRkYyIsInVuaXF1ZV9uYW1lIjoiaXJ3aW4ud2lsbGlhbXNAdGVsZWlvcy1zeXN0ZW1zLmNvbSIsInVwbiI6Imlyd2luLndpbGxpYW1zQHRlbGVpb3Mtc3lzdGVtcy5jb20iLCJ1dGkiOiJvaWlTRnd3MlBrT1pibUdZVU50ZUFBIiwidmVyIjoiMS4wIiwieG1zX3N0Ijp7InN1YiI6ImRTQWdJOVNVaE8tVHpGVzJsbko3M05HdFhSdjg3RHJiVmFrQUkzNHh4bG8ifSwieG1zX3RjZHQiOjEzMTg4MTI4MzV9.fNbkK2Rd2wH047lwtC9He-bjg0AgVLWQ4Ql3nmWt5wubsJnlfCL2kY-0LCrDdb5xPajQuQCoFeyL0GKmmmZGV-6ddeBEcmnJ3tU8pd8ZLD5iN_Ub2YEbei8fF1HOCJMDMJwKihyX9MYq8sK8ylPXXPH3UOJacpNHujUmXr3mrrowW8dlLPorOINzl5XqMEY7ImsXkpuswnQDO2z6uH4GmWLmEVC_mxgs3bbmEGg7Ly7XuzsfM0x-CtrtUjEG96_wemE7niqXSbZJ3aWoCpmEslzhoR5napFA0tJ5kstlvGsIqE_uj3yumqKJALYJjHDm5s0r6h0J_7eOJGh3eO_zCQ";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                    .AuthenticationHeaderValue("Bearer", token);

                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue { }
                //$headers = @{ "Authorization" = "bearer " +  $tokenInfo.access_token}

                c = await client.GetStringAsync("https://management.azure.com/subscriptions?api-version=2019-06-01");
            }

            Console.WriteLine(c);
        }
    }
}

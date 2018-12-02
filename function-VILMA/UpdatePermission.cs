using Microsoft.WindowsAzure.Storage.Table;

namespace Shelly.Bot
{
    public class UpdatePermission: TableEntity
    {
        public string UpdatesUserId
        {
            get { return RowKey; }
            set { RowKey = value; }
        }
        public dynamic UserId
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }
    }
}
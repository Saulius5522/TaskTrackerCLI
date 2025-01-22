namespace TaskTrackerCLI
{
    public class Task_
    {
        public int ID { get; private set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; private set; }
        public DateTime Updated { get; set; }

        public Task_(int id, string description)
        {
            this.ID = id;
            this.Description = description;
            this.Status = "ToDo";
            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Task ID:{this.ID} \"{this.Description}\" {Status}";
        }
    }
}

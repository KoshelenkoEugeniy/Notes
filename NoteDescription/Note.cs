using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace NoteDescription
{
    [DataContract]
    //[KnownType(typeof(NoteStatus))]
    public class Note
    {
        [DataMember]
        public DateTime DateOfCreation { get; set; }

        [DataMember]
        public string Title { get; set; }

        //[DataMember]
        //public NoteStatus Status { get; set; }

        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public int NoteNumber { get; set; }

        public Note(String title, int number)
        {
            DateOfCreation = DateTime.Now;
            Title = title;
            //Status = NoteStatus.Current;
            Status = "Current";
            NoteNumber = number;
        }

        public override string ToString()
        {
            return $"Note: {NoteNumber}         Created: {DateOfCreation.ToString(@"dd\/MM\/yyyy HH\:mm\:ss tt")}       Status: {Status} \n \n {Title} \n \n";
        }
    }

    //[DataContract]
    //public enum NoteStatus
    //{
    //    Finished,
    //    Current
    //}
}

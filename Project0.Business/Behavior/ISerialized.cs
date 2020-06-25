
namespace Project0.Business.Behavior {

    /// <summary>
    /// INterface that guarantees an ID field for the purpose
    /// of being able to serialize and deserialize objects correctly
    /// </summary>
    public interface ISerialized {

        /// <summary>
        /// The object's unique ID
        /// </summary>
        ulong ID { get; set; }
    }
}

using HotChocolate.Language;

namespace HotChocolateParseLong
{
    public class Query
    {
        public Book GetBook() =>
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }
            };
    }

    /// <summary>
    /// The goal is that anywhere a long is specified as a input variable to a mutation, the schema
    /// would be built with type ID since client-side everything is strings. 
    /// </summary>
    public class Mutation
    {
        public bool EditBooks(List<long> ids)
        {
            return true;
        }

        public bool EditBook(long id)
        {
            return true;
        }
    }
    /// <summary>
    /// This one does not work with List<long>
    /// </summary>
    //public class SnowflakeIdType : IdType
    //{
    //    public override object? ParseLiteral(IValueNode literal)
    //    {
    //        return literal switch
    //        {
    //            null => throw new ArgumentNullException(nameof(literal)),
    //            StringValueNode stringLiteral when long.TryParse(stringLiteral.Value, out var longVal) => longVal,
    //            IntValueNode intValueNode when long.TryParse(intValueNode.Value, out var longVal) => longVal,
    //            _ => throw new SerializationException(new Error($"Can not parse {Name}"), this)
    //        };
    //    }
    //}


    /// <summary>
    /// This does with with List<long>. But I need to use this constructor and string "ID" to get the schema to build with the IDType.
    /// </summary>
    public class SnowflakeIdType : ScalarType<long>
    {   
        public SnowflakeIdType() : base("ID")
        {
        }

        public override bool IsInstanceOfType(IValueNode valueSyntax)
        {
            throw new NotImplementedException();
        }

        public override object? ParseLiteral(IValueNode literal)
        {
            return literal switch
            {
                null => throw new ArgumentNullException(nameof(literal)),
                StringValueNode stringLiteral when long.TryParse(stringLiteral.Value, out var longVal) => longVal,
                IntValueNode intValueNode when long.TryParse(intValueNode.Value, out var longVal) => longVal,
                _ => throw new SerializationException(new Error($"Can not parse {Name}"), this)
            };
        }

        public override IValueNode ParseResult(object? resultValue)
        {
            throw new NotImplementedException();
        }

        public override IValueNode ParseValue(object? runtimeValue)
        {
            throw new NotImplementedException();
        }
    }
}

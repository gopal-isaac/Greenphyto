namespace Common
{
    /// <summary>
    /// A generic reponse to reduce decoupling with the help of Type
    /// My previous architect designed this and it has worked beautifully with mediatR  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        // Constructor to set success state and message
        public GenericResponse(T data, bool success = true, string message = "")
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public static GenericResponse<T> Failure(string message)
        {
            return new GenericResponse<T>(default(T), false, message);
        }

        public static GenericResponse<T> SuccessResponse(T data, string message = "")
        {
            return new GenericResponse<T>(data, true, message);
        }
    }
}

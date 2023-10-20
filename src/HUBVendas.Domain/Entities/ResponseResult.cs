using System;

namespace HUBVendas.Domain.Entities {
    public class ResponseResult<T> {
        public ResponseInfo Info { get; set; } = new ResponseInfo();
        public string Date { get => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); }
        public T? Data { get; set; }

        public void SetErrors(List<string> message) {
            Info.Messages = message;
            Info.Success = false;
            Data = default;
        }
        public void SetError(string message) {
            Info.Messages = message ;
            Info.Success = false;
            Data = default;
        }

        public void SetSucess(string message, T? data = default) {
            Info.Messages = message ;
            Info.Success = true;
            Data = data;
        }
    }

    public class ResponseInfo {
        public bool Success { get; set; } = true;

        public object? Messages { get; set; }
    }
}
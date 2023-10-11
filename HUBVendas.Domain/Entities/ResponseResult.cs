using System;

namespace HUBVendas.Domain.Entities {
    public class ResponseResult<T> where T : Entity {
        public ResponseInfo Info { get; set; } = new ResponseInfo();
        public string Date { get => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); }
        public T? Data { get; set; }

        public void SetError(string message) {
            Info.Message = message;
            Info.Success = false;
            Data = null;
        }
    }

    public class ResponseInfo {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "Operação realizada com sucesso!";
    }
}
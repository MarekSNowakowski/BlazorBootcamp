﻿using Microsoft.AspNetCore.Components.Forms;

namespace BlazorBootcampWeb_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string file);
    }
}

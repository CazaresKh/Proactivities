using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Photos.Commands
{
    public class PhotoAddCommand : IRequest<Result<Photo>>
    {
        public IFormFile File { get; set; }
    }
}
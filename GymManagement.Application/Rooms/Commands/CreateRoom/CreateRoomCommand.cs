using ErrorOr;
using GymManagement.Domain.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(Guid GymId, string RoomName) : IRequest<ErrorOr<Room>>;
}

using Auth.Model;
using Auth.Request;
using AutoMapper;

namespace Auth.Profiles
{
    public class AuthProfiles: Profile
    {
        public AuthProfiles()
        {
            CreateMap<AddUser, User>().ReverseMap();

        }
    }
}

using AutoMapper;
using UserMicroservice.Entities;
using UserMicroservice.Models.Requests;
using UserMicroservice.Models.Responses;

namespace UserMicroservice.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<User, UpdateUserRequest>();
        CreateMap<User, CreateUserResponse>();
        CreateMap<User, GetUserResponse>();

        CreateMap<CreateSubscriptionRequest, Subscription>();
        CreateMap<Subscription, CreateSubscriptionResponse>();
    }
}
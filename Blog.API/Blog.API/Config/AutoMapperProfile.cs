using AutoMapper;
using Blog.Application.Dto;
using Blog.Core.Helper;
using Blog.Domain.Entities;
using System;

namespace Blog.WebAPI.Config
{
    /// <summary>
    /// AutoMapper
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ContactUs, ContactUsDto>().ReverseMap();
            CreateMap<ContactUs, ContactUsItem>().ReverseMap(); 
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserItem>().ReverseMap();
            CreateMap<Dictionary, DictionaryDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();
            CreateMap<Material, MaterialDto>().ReverseMap();
            CreateMap<Material, MaterialItem>().ReverseMap();
            CreateMap<Keywords, KeywordsDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Article, ArticleItem>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CommentItem>().ReverseMap();
            CreateMap<Interaction, InteractionItem>().ReverseMap();
            CreateMap<Interaction, InteractionDto>().ReverseMap();



        }
        /// <summary>
        /// DateTimeTypeConverter
        /// </summary>
        public class DateTimeTypeConverter : ITypeConverter<int, DateTime>
        {
            /// <summary>
            /// DateTimeTypeConverter
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public DateTime Convert(int source, DateTime destination, ResolutionContext context)
            {
                return UnixTime.FromTimeStamp(source);
            }
        }

        /// <summary>
        /// DateTimeTypeReverseConverter
        /// </summary>
        public class DateTimeTypeReverseConverter : ITypeConverter<DateTime, int>
        {
            /// <summary>
            /// Convert
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            public int Convert(DateTime source, int destination, ResolutionContext context)
            {
                return UnixTime.ToTimeStamp(source);
            }
        }

        public class DateTimeTypeStringConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return UnixTime.FromTimeString(source);
            }
        }

        public class DateTimeTypeStringReverseConverter : ITypeConverter<DateTime, string>
        {
            public string Convert(DateTime source, string destination, ResolutionContext context)
            {
                return UnixTime.ToTimeString(source);
            }
        }

        public class IntTypeStringConverter : ITypeConverter<int, string>
        {
            public string Convert(int source, string destination, ResolutionContext context)
            {
                string result = "";
                if (source < 1000000000)
                {
                    result = source.ToString();
                }
                else
                {
                    result = UnixTime.FromTimeStamp(source).ToString();
                }
                return result;
            }
        }

        public class IntTypeStringReverseConverter : ITypeConverter<string, int>
        {
            public int Convert(string source, int destination, ResolutionContext context)
            {
                int result = 0;
                if (int.Parse(source) < 1000000000)
                {
                    result = int.Parse(source);
                }
                else
                {
                    DateTime date = UnixTime.FromTimeString(source);
                    result = UnixTime.ToTimeStamp(date);
                }
                return result;

            }
        }
    }
}

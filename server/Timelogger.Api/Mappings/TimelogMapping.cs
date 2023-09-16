using System;
using Timelogger.Api.Dtos;
using Timelogger.Entities;

namespace Timelogger.Api.Mappings
{
    public static class TimelogMapping {
        public static ProjectTimelogGetResponseDto ToProjectTimelogGetResponseDto(this Timelog timelog){
            
            return new ProjectTimelogGetResponseDto {
                Id = timelog.Id,
                ProjectId = timelog.ProjectId,
                DurationMinutes = timelog.DurationMinutes,
                Note = timelog.Note,
                CreatedAt = timelog.CreatedAt,
            };
        }

        public static TimelogInsertResponseDto ToTimelogInsertResponseDto(this Timelog timelog){
            
            return new TimelogInsertResponseDto {
                Id = timelog.Id,
                ProjectId = timelog.ProjectId,
                DurationMinutes = timelog.DurationMinutes,
                Note = timelog.Note,
                ProjectFinished = timelog.Project?.ClosedAt.HasValue ?? false,
            };
        }

        public static Timelog ToTimelog(this TimelogInsertRequestDto timelogRequest, Guid projectId){
            
            return new Timelog {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                DurationMinutes = timelogRequest.DurationMinutes,
                Note = timelogRequest.Note
            };
        }
    }   
}
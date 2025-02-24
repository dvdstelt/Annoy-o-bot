﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace Annoy_o_Bot.Tests
{
    public class JsonReminderParserTests
    {
        Reminder reminder = new Reminder
        {
            Title = "The title",
            Message = "A message",
            Assignee = "SomeUserHandle;AnotherUserHandle",
            Interval = Interval.Monthly,
            IntervalStep = 5,
            Date = new DateTime(2010, 11, 12)
        };

        readonly JsonReminderParser jsonReminderParser = new JsonReminderParser();

        [Fact]
        void Should_parse_reminder_correctly()
        {
            var input = JsonConvert.SerializeObject(reminder);

            var result = jsonReminderParser.Parse(input);

            Assert.Equal("The title", result.Title);
            Assert.Equal("A message", result.Message);
            Assert.Equal("SomeUserHandle;AnotherUserHandle", result.Assignee);
            Assert.Equal(Interval.Monthly, result.Interval);
            Assert.Equal(5, result.IntervalStep);
            Assert.Equal(new DateTime(2010, 11, 12), reminder.Date);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData(null)]
        void Must_provide_a_title(string title)
        {
            reminder.Title = title;
            var input = JsonConvert.SerializeObject(reminder);

            var ex = Assert.Throws<ArgumentException>(() => jsonReminderParser.Parse(input));
            Assert.Contains("A reminder must provide a non-empty Title property", ex.Message);
        }

        [Theory]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        [InlineData(Interval.Yearly)]
        [InlineData(Interval.Once)]
        void Should_parse_interval_int_value(Interval interval)
        {
            reminder.Interval = interval;
            var input = JsonConvert.SerializeObject(reminder);

            var result = jsonReminderParser.Parse(input);

            Assert.Equal(interval, result.Interval);
        }

        [Theory]
        [InlineData(Interval.Daily)]
        [InlineData(Interval.Weekly)]
        [InlineData(Interval.Monthly)]
        [InlineData(Interval.Yearly)]
        [InlineData(Interval.Once)]
        void Should_parse_interval_string_value(Interval interval)
        {
            reminder.Interval = interval;
            var input = JsonConvert.SerializeObject(reminder, new StringEnumConverter(false));

            var result = jsonReminderParser.Parse(input);

            Assert.Equal(interval, result.Interval);
        }
    }
}

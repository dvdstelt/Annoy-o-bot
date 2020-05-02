# Annoy-o-Bot

This is a simple GitHub app which can create issues on pre-defined schedules for you. Use this app to check-in and manage reminders and other tasks in source-control.

## Create a reminder

Each reminder is represented by a unique JSON document inside the `.reminders` folder. The app only takes reminders commited to the default branch (typically `master`) into account.

The JSON file supports the following properties:

| Property | Type | Required | Description |
| --- | --- | --- | --- |
| title | string | mandatory | The title of the issue being created. |
| message | string | mandatory | The issue description for the issue being created. |
| assignee | string | optional | A `;` deliminited list of users/teams being assigned to the issue. |
| date | string | mandatory | A [ISO 8601 standard](http://en.wikipedia.org/wiki/ISO_8601) date string indicating when the issue should be created the first time. |
| interval | digit | mandatory | Defines the interval granularity in which a new issue should be created after the first date. See [interval description](#intervals) for more detail. |
| intervalStep | digit | optional | Defines how many intervals should be used before raising the next issue. This allows to customize intervals to something like "every 3 months", "every second week" and so on. |

### Intervals

The following intervals are supported:

| Digit | Interval |
| --- | --- |
| 0 | Only once |
| 1 | Daily |
| 2 | Weekly |
| 3 | Monthly |
| 4 | Yearly |

### Example

```json  
{
  "title": "My first reminder",
  "message": "hello world!",
  "assignee": "johndoe",
  "date": "2020-01-01",
  "interval": 3
}
```

This sample will immediately create a new issue (because the specified start date already passed) with the title `My first reminder` and assign it to the github user `johndoe`. It will then re-create this issue every month on the first of the month.

## Update a reminder

Update an existing reminder document to change the scheduled interval or issue details to be created.

## Delete a reminder

Delete the reminder document you want to remove.


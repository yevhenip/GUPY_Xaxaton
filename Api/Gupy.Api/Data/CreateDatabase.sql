create schema gupy_events;

create table gupy_events.events
(
    Id              int auto_increment
        primary key,
    Name            varchar(100) default 'New Event' not null,
    Description     varchar(255) default ''          not null,
    SubscribedCount int          default 0           null,
    MinWantedPeople int                              null,
    Category        varchar(100) default ''          not null,
    Type            int                              null
);

create table gupy_events.users
(
    Id       int auto_increment
        primary key,
    Name     varchar(100) null,
    UserName varchar(255) null,
    Phone    varchar(100) null
);



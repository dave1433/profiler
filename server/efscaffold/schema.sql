drop schema if exists profilersystem cascade;
create schema if not exists profilersystem;

create table profilersystem.profiler(
                                        id serial primary key,
                                        Firstname varchar(100) not null,
                                        Lastname varchar(100) not null,
                                        Age int not null,
                                        Occupation varchar(150) not null,
                                        City varchar(100)not null,
                                        PhotoURl text,
                                        CreatedAt timestamp default now(),
                                        UpdatedAt timestamp default now()

);

select * from profilersystem.profiler;

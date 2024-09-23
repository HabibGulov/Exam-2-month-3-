create database task_management_db;

create table users (
    id uuid primary key,
    username varchar(255) unique not null,
    email varchar(255) unique not null,
    passwordhash varchar(255) not null,
    createdat date 
);

create table categories (
    id uuid primary key,
    name varchar(255) unique not null,
    createdat date
);

create table tasks (
    id uuid primary key,
    title varchar(255) not null,
    description text,
    iscompleted boolean,
    duedate date,
    userid uuid,
    categoryid uuid,
    priority integer,
    createdat date,
    foreign key (userid) references users(id),
    foreign key (categoryid) references categories(id)
);

create table comments (
    id uuid primary key,
    taskid uuid,
    userid uuid,
    content text not null,
    createdat date,
    foreign key (taskid) references tasks(id),
    foreign key (userid) references users(id)
);

create table taskattachments (
    id uuid primary key,
    taskid uuid,
    filepath varchar(255) not null,
    createdat date,
    foreign key (taskid) references tasks(id)
);

create table taskhistory (
    id uuid primary key,
    taskid uuid,
    changedescription text not null,
    changedat date,
    foreign key (taskid) references tasks(id)
);
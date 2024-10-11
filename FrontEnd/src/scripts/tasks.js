import { getCookieByName } from "./helpers"
import { showTask, shownTask, tasksChanged } from "./UIcontroller"

export const addTask = async (title, groupId, myDay) =>
{
    const data = new FormData()
    data.append('title', title)
    data.append('groupId', groupId)
    data.append('myDay', myDay)

    const request = await fetch("https://localhost:5000/task/create", {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
        body: data
    })
    if (request.ok)
    {
        return await request.json()
    }
}
export const removeTask = async (taskId) =>
{
    const data = new FormData()
    data.append('taskId', taskId)

    const request = await fetch("https://localhost:5000/task/delete", {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
        body: data
    })
    if (request.ok)
    {
        return await request.json()
    }
}
export const patchTask = async (task) =>
{
    console.log(task)
    const t = {
        Id:task.id, 
        Title:task.title, 
        IsCompleted:task.isCompleted, 
        RepeatType:(task.repeatType == null ? 0 : task.repeatType), 
        MyDay: task.myDay 
    }
    console.log(task.dueTo)
    if (!task.dueTo.startsWith('00'))
        t.DueTo = (new Date(task.dueTo)).toISOString();
    console.log(t)

    const request = await fetch("https://localhost:5000/task/patch", {
        method: 'PATCH',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`,
            'Content-Type': 'application/json' // Tell the server you're sending JSON
        }, 
        body: JSON.stringify(t)
    })
    if (request.ok && shownTask.value != null && task.id == shownTask.value.id)
    {
        const ts = await request.json()
        ts.repeat = ts.repeatType;
        console.log(ts);
        showTask(ts)
        tasksChanged.value = true

    }
}
export const getTasks = async (groupId) => 
{
    const data = new FormData()
    data.append('groupId', groupId)
    const request = await fetch("https://localhost:5000/task/getgroup",{
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
        body: data
    })
    if (request.ok)
    {
        return await request.json();
    }
}

export const getBaseTaskCount = async () =>
{
    const request = await fetch("https://localhost:5000/task/basecount",{
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
    })
    if (request.ok)
    {
        return await request.json();
    }
}
export const getMyDayTasks = async () =>
{
    const request = await fetch("https://localhost:5000/task/myday",{
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
    })
    if (request.ok)
    {
        return await request.json();
    }
}
export const getPlannedTasks = async () =>
{
    const request = await fetch("https://localhost:5000/task/planned",{
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
    })
    if (request.ok)
    {
        return await request.json();
    }
}
export const getCompletedTasks = async () =>
{
    const request = await fetch("https://localhost:5000/task/completed  ",{
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
    })
    if (request.ok)
    {
        return await request.json();
    }
}
import { ref } from "vue"
import { getCookieByName } from "./helpers"

export const groups = ref([])
export const addGroup = async (title) =>
{
    const data = new FormData()
    data.append('title', title)

    const request = await fetch("https://localhost:5000/group/create", {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
        body: data
    })
    if (request.ok)
    {
        const j = await request.json()
        groups.value.push(j.group)
        return j.group
    }
}
export const renameGroup = async (groupId, newTitle) =>
{
    const data = new FormData()
    data.append('groupId', groupId)
    data.append('newTitle', newTitle)

    const request = await fetch("https://localhost:5000/group/rename", {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        },
        body: data
    })
    if (request.ok)
    {
        let group = groups.value.find(t => t.id == groupId);
        group = await request.json()
    }
}
export const getGroups = async () => {
    const request = await fetch("https://localhost:5000/group/get", {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${getCookieByName('JWT')}`
        }
    })
    if (request.ok)
    {
        groups.value = await request.json() 
    }
}
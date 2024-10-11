import { ref } from "vue"

export const user = ref()

export const restart = async () => {
    const request = await fetch("https://localhost:5000/account/restart",{
        method:"GET",
        }
    )
}

export const register = async (login,fullName, password) => 
{
    const data = new FormData()
    data.append("login", login)
    data.append("fullName", fullName)
    data.append("password", password)
    const request = await fetch("https://localhost:5000/account/register", {
        method: "POST",
        body: data
    })

    if (request.ok)
    {
        console.log("Registered")
    }
}
export const login = async (login, password) =>
{
    const data = new FormData()
    data.append("login", login)
    data.append("password", password)

    const request = await fetch("https://localhost:5000/account/login", {
        method: "POST",
        body: data
    })
    if (request.ok)
    {
        var expires = (new Date(Date.now()+ 300*1000)).toUTCString();
  
        const j = await request.json()
        document.cookie = `JWT=${j.access_token}; expires=${expires}`
        user.value = {'login': j.username, 'fullName': j.fullName}
        console.log(user.value)
        return 'ok'
    }
    else
        return "unreal"
}
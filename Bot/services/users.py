import aiohttp
from config import API_URL
from models.api import User


class UsersService:
    def __init__(self):
        connector = aiohttp.TCPConnector(verify_ssl=False)
        self._session = aiohttp.ClientSession(connector=connector)

    async def register_user(self, user: User) -> bool:
        async with self._session.post(f"{API_URL}/users", data=user.to_json()) as resp:
            return resp.status == 201

    async def user_is_registered(self, user_telegram_id: int) -> bool:
        async with self._session.post(f"{API_URL}/users/{user_telegram_id}") as resp:
            return resp.status == 200

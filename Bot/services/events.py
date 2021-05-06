import aiohttp
from typing import List
from config import API_URL
from models.api import Event


class EventsService:
    def __init__(self):
        connector = aiohttp.TCPConnector(verify_ssl=False)
        self._session = aiohttp.ClientSession(connector=connector)

    async def get_events(self, page=1) -> List[Event]:
        async with self._session.get(f"{API_URL}/events/page/{page}") as resp:
            return [Event.from_dict(event) for event in await resp.json()]

    async def get_user_events(self, user_telegram_id):
        async with self._session.get(f"{API_URL}/events/my/{user_telegram_id}") as resp:
            return [Event.from_dict(event) for event in await resp.json()]

    async def subscribe_to_event(self, event_id: int, user_telegram_id: int) -> bool:
        async with self._session.put(f"{API_URL}/events/{event_id}/{user_telegram_id}") as resp:
            return resp.status == 200

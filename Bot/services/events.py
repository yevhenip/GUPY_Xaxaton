import aiohttp
from typing import List
from config import API_URL
from models.api import Event


class EventsService:
    def __init__(self):
        connector = aiohttp.TCPConnector(verify_ssl=False)
        self._session = aiohttp.ClientSession(connector=connector)

    async def get_events(self, page=1) -> List[Event]:
        async with self._session.get(f"{API_URL}/events?page={page}") as resp:
            return [Event.from_dict(event) for event in await resp.json()]

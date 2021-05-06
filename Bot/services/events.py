import aiohttp


class EventsService:
    def __init__(self):
        self._session = aiohttp.ClientSession()

    async def get_events(self):
        async with self._session.get('http://httpbin.org/get') as resp:
            print(resp.status)
            print(await resp.text())

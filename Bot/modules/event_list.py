from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.dispatcher.filters.state import State, StatesGroup
from aiogram.types import Message, ReplyKeyboardMarkup, ParseMode

from dependency_injector.wiring import inject, Provide

from container import DiContainer
from services.events import EventsService


class EventListStates(StatesGroup):
    scrolling = State()


def get_navigation_keyboard(page_number: int):
    keyboard = ReplyKeyboardMarkup(resize_keyboard=True)
    options = ["<<<", str(page_number), ">>>"]
    keyboard.add(*options)

    return keyboard


@inject
async def list_events(msg: Message, state: FSMContext,
                      event_service: EventsService = Provide[DiContainer.events_service]):
    await EventListStates.scrolling.set()

    events = await event_service.get_events()
    keyboard = get_navigation_keyboard(1)

    event_view = []
    for event in events:
        event_view.append(f"<b>{event.name}</b>\n<i>{event.event_time}</i>\n{event.description}")

    await msg.answer("\n\n".join(event_view), reply_markup=keyboard, parse_mode=ParseMode.HTML)


def register_event_list_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(list_events, commands=["events"], state="*")

from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.dispatcher.filters.state import State, StatesGroup
from aiogram.types import Message, ReplyKeyboardMarkup, InlineKeyboardMarkup, InlineKeyboardButton, ParseMode, \
    CallbackQuery

from dependency_injector.wiring import inject, Provide

from container import DiContainer
from services.events import EventsService


class EventListStates(StatesGroup):
    scrolling = State()


def get_navigation_keyboard():
    keyboard = ReplyKeyboardMarkup(resize_keyboard=True)
    options = ["/previous", "/next"]
    keyboard.add(*options)
    keyboard.row("/cancel")

    return keyboard


@inject
async def list_events(msg: Message,
                      state: FSMContext,
                      event_service: EventsService = Provide[DiContainer.events_service]):
    await EventListStates.scrolling.set()

    keyboard = get_navigation_keyboard()
    await msg.answer("Показую івенти:", reply_markup=keyboard)

    events = await event_service.get_events()
    for event in events:
        event_type = "Онлайн" if event.type == 0 else "Оффлайн"

        reactions = [
            InlineKeyboardButton(text=f"👍 Піду!", callback_data=f"{event.id}")
        ]

        markup = InlineKeyboardMarkup()
        markup.add(*reactions)

        await msg.answer(
            f"📎 Назва: <b>{event.name}</b>\n\n"
            f"📅 Дата проведення: <i>{event.event_time}</i>\n"
            f"⌚ Тривалість: <i>{event.duration}</i> год.\n"
            f"⭕ Тип: {event_type}\n"
            f"🧍 Людей підуть: {event.subscribed_count}\n\n"
            f"📜 Опис:\n<code>{event.description}</code>",
            reply_markup=markup,
            parse_mode=ParseMode.HTML)


async def handle_event_subscription(callback: CallbackQuery):
    # call api and add person to event
    await callback.answer(text="Тебе було додано до івенту! Перевір особистий кабінет (/myevents)",
                          show_alert=True)


def register_event_list_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(list_events, commands=["events"], state="*")
    dispatcher.register_callback_query_handler(handle_event_subscription, state=EventListStates.scrolling)

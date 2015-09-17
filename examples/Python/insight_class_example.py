from Insight import *

# instantiate Insight class
insight = Insight(composerConnect=True)

# connect insight instance to Xavier composer
insight.connect()

# set of operations to get state from Insight
# returns 0 if successful
state = insight.get_state(insight.eEvent)

# event types IEE_Event_t returns 64 if EmoStateUpdated
event = insight.get_event_type(insight.eEvent)
